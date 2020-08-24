using Demo.DTO.BasicsDataDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IDomian.IDM
{
    public interface IBasicsDataDM
    {
        List<XianluDTO> GetXianluList(out int count);

        List<BiaoduanDTO> GetBiaoduanList(Guid? xianluId, out int count);

        List<GongdianDTO> GetGongdianList(Guid? biaoduanID, out int count);
    }
}
