using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace ForumDEG.MarkupExtensions {
    class EmbeddedImage : IMarkupExtension {
        public string ResourceId { get; set; } 

        public object ProvideValue(IServiceProvider serviceProvider) {
            if(String.IsNullOrWhiteSpace(ResourceId)) return null;
            return ImageSource.FromResource(ResourceId);
        }
    }
}
