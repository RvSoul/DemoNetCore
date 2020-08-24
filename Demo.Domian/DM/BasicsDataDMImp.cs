using Demo.DTO;
using Demo.DTO.BasicsDataDTO;
using Demo.DTO.ModelData;
using Demo.IDomian.IDM;
using Demo.Model.CM;
using Demo.Model.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Demo.Domian.DM
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicsDataDMImp : IBasicsDataDM//BaseDM//
    {

        private readonly TodoContext c;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public BasicsDataDMImp(TodoContext context)
        {
            c = context;
        }


        public List<XianluDTO> GetXianluList(out int count)
        {
            LogUtil.Info("BasicsDataDMImp类-GetXianluList方法。");
             
            count = c.Xianlu.Count();
            List<Xianlu> li = c.Xianlu.ToList();
            // int a = Convert.ToInt32("qwe ");
            return GetMapperDTO.GetDTOList<Xianlu, XianluDTO>(li); ;
        }

        public List<BiaoduanDTO> GetBiaoduanList(Guid? xianluId, out int count)
        {
            List<BiaoduanDTO> dtoli = new List<BiaoduanDTO>();

            count = c.Xianlu.Count();
            List<Biaoduan> li = c.Biaoduan.Where(w => w.XianluId == xianluId).ToList();
            dtoli = GetMapperDTO.GetDTOList<Biaoduan, BiaoduanDTO>(li);

            return dtoli;
        }

        public List<GongdianDTO> GetGongdianList(Guid? biaoduanID, out int count)
        {
            List<GongdianDTO> dtoli = new List<GongdianDTO>();

            count = c.Xianlu.Count();
            List<Gongdian> li = c.Gongdian.Where(w => w.BiaoduanID == biaoduanID).ToList();
            dtoli = GetMapperDTO.GetDTOList<Gongdian, GongdianDTO>(li);

            return dtoli;
        }
    }
}
