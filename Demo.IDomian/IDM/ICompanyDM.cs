using Demo.DTO.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IDomian.IDM
{
    public interface ICompanyDM
    {
        bool AddShigong(ShigongModel model);

        List<ShigongDTO> GetShigongList(Request_Shigong dto, out int count);

        bool UpShigong(ShigongModel model);

        bool DeShigong(Guid id);

        bool AddShigongImages(ShigongImagesModel model);

        List<ShigongImagesDTO> GetShigongImagesList(Guid id);

        bool DeShigongImages(Guid id);

        bool AddShigongEnclosure(ShigongEnclosureModel model);

        List<ShigongEnclosureDTO> GetShigongEnclosureList(Guid id);

        bool DeShigongEnclosure(Guid id);
    }
}
