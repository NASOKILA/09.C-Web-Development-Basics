namespace SimpleMvs.Framework.Models
{
    using System.Collections.Generic;

    public class ViewModel
    {
        public ViewModel()
        {
            this.Data = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Data { get; }

        //with this syntax we index it upon initialization
        public string this[string key]
        {
            get => this.Data[key]; //we get the data key
            set => this.Data[key] = value; //we set it to the value
        }
        //we can directly access the inner propertirs
    }
}
