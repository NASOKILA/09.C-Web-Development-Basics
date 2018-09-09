using HTTPServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Application.Views.Cake
{
    public class UpdatedCake : IView
    {

        public string Name { get; set; }
        public string Price { get; set; }

        public UpdatedCake(string name, string price)
        {
            this.Name = name;
            this.Price = price;
            View();
        }

        public string View()
        {
            var result = System.IO.File.ReadAllText("./Application/Resourses/add.html");
            result = result.Replace(
                "<div id=\"currentCake\"></div>", 
                $"<p>name: {this.Name}<br/>price: ${this.Price}</p>");

            return result;
        }
    }
}
