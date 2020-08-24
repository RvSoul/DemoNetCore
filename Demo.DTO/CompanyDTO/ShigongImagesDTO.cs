using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO.CompanyDTO
{
    public class Request_ShigongImages
    {

    }
    public class ShigongImagesModel
    {
        public System.Guid Id { get; set; }
        public System.Guid ShigongId { get; set; }
        public string ImgUrl { get; set; }
    }
    public class ShigongImagesDTO : ShigongImagesModel
    { }
}
