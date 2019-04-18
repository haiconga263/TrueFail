using GS1.UI.ProductionImages.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS1.UI.ProductionImages.ViewModels
{
    public class ProductionImageData : ProductionImage
    {
        public string ImageDataFront { set; get; }
        public bool IsImageFrontChanged { set; get; }

        public string ImageDataLeft { set; get; }
        public bool IsImageLeftChanged { set; get; }

        public string ImageDataTop { set; get; }
        public bool IsImageTopChanged { set; get; }

        public string ImageDataBack { set; get; }
        public bool IsImageBackChanged { set; get; }

        public string ImageDataRight { set; get; }
        public bool IsImageRightChanged { set; get; }

        public string ImageDataBottom { set; get; }
        public bool IsImageBottomChanged { set; get; }
    }
}
