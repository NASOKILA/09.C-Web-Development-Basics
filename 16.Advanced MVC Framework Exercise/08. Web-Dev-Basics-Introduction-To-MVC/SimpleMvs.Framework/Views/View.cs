namespace SimpleMvs.Framework.Views
{
    using Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    //Loads layout.html and renders other html views in it.
    public class View : IRenderable
    {
        public string BaseLayoutFileName = "Layout";
        public string ContentPlaceHolder = "{{{content}}}";
        public string HtmlExtension = ".html";
        public string LocalErrorPath = "\\SimpleMvs.Framework\\Errors\\Error.html";

        private readonly string templateFullQualifiedName;
        private readonly IDictionary<string, string> viewData;

        public View(string templateFullQualifiedName, 
            IDictionary<string, string> viewData)
        {
            this.templateFullQualifiedName = templateFullQualifiedName;
            this.viewData = viewData;
        }

        //returns the full html
        public string Render()
        {
            string fullHtml = this.ReadFile();

            if (this.viewData.Any()) {

                foreach (var parameter in this.viewData)
                {
                    //we replace the key eith the value
                    fullHtml = fullHtml.Replace($"{{{{{{{parameter.Key}}}}}}}", parameter.Value);
                }
            }

            return fullHtml;
        }

        private string ReadFile()
        {
            string layoutHtml = this.RenderLayoutHtml();

            string templateFullQualifiedNameWithExtenion = this.templateFullQualifiedName + HtmlExtension;

            if (!File.Exists(templateFullQualifiedNameWithExtenion))
            {
                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);
                this.viewData.Add("error", "Requested view does not exist!");
                return errorHtml;
            }

            //to do
            string fileRead = File.ReadAllText(templateFullQualifiedNameWithExtenion);
            string fullHtml = layoutHtml.Replace(this.ContentPlaceHolder, fileRead);
            return fullHtml;
        }

        private string RenderLayoutHtml()
        {
            string layoutHtmlQualifiedName = string.Format(
                "{0}\\{1}{2}",
                MvcContext.Get.ViewsFolder,
                BaseLayoutFileName,
                HtmlExtension
            );


            if (!File.Exists(layoutHtmlQualifiedName)) {

                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);
                this.viewData.Add("error", "Layout view does not exist!");
                return errorHtml;
            }

            string layoutHtmlFileContent = File.ReadAllText(layoutHtmlQualifiedName);

            return layoutHtmlFileContent;
        }

        private string GetErrorPath()
        {
            string appDirectoryPath = Directory.GetCurrentDirectory();
            DirectoryInfo parentDirectory = Directory.GetParent(appDirectoryPath);
            string parentDirectoryPath = parentDirectory.FullName;

            string errorPagePath = parentDirectoryPath + LocalErrorPath;
            return errorPagePath;
        }
    }
}
