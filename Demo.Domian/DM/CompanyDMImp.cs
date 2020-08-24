using Demo.DTO.CompanyDTO;
using Demo.DTO.ModelData;
using Demo.IDomian.IDM;
using Demo.Model.CM;
using Demo.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Demo.Domian.DM
{
    public class CompanyDMImp : ICompanyDM
    {
        private readonly TodoContext c;
        public CompanyDMImp(TodoContext context)
        {
            c = context;
        }

        public bool AddShigong(ShigongModel model)
        {


            model.Id = Guid.NewGuid();
            int code;
            try
            {
                code = c.Shigong.Max(w => w.Code);
            }
            catch (Exception)
            {
                code = 1;
            }
            model.Code = code + 1;
            Shigong pt = GetMapperDTO.SetModel<Shigong, ShigongModel>(model);

            c.Shigong.Add(pt);
            c.SaveChanges();
            return true;

        }

        public List<ShigongDTO> GetShigongList(Request_Shigong dto, out int count)
        {
            List<ShigongDTO> dtoli = new List<ShigongDTO>();

            Expression<Func<Shigong, bool>> expr = AutoAssemble.Splice<Shigong, Request_Shigong>(dto);
            if (dto.BShiyongTime2 != null)
            {
                expr = expr.And2(w => w.ShiyongTime2 >= dto.BShiyongTime2);
            }
            if (dto.EShiyongTime2 != null)
            {
                expr = expr.And2(w => w.ShiyongTime2 <= dto.EShiyongTime2);
            }
            //throw new  Exception("11111111111");
            count = c.Shigong.Where(expr).Count();
            //List<Shigong> li = c.Shigong.Where(expr).OrderBy(px => px.Code).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            dtoli = c.Shigong.Where(expr).OrderBy(px => px.Code).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize)
                .Select(x => new ShigongDTO
                {
                    BiaoduanId = x.BiaoduanId,
                    BiaoduanName = x.Biaoduan.BiaoduanName,
                    Code = x.Code,
                    Id = x.Id,
                    Money = x.Money,
                    PayDuixiang = x.PayDuixiang,
                    RegisterPeople = x.RegisterPeople,
                    Remarks = x.Remarks,
                    ShenhePeople = x.ShenhePeople,
                    ShiyongTime = x.ShiyongTime,
                    ShiyongTime2 = x.ShiyongTime2,
                    Yongtu = x.Yongtu
                }).ToList();
            //GetMapperDTO.GetDTOList<Shigong, ShigongDTO>(li);
            return dtoli;



        }

        public bool UpShigong(ShigongModel model)
        {

            Shigong pt = c.Shigong.FirstOrDefault(n => n.Id == model.Id);
            pt.Money = model.Money;
            pt.BiaoduanId = model.BiaoduanId;
            //pt.Code = model.Code;
            pt.PayDuixiang = model.PayDuixiang;
            pt.RegisterPeople = model.RegisterPeople;
            pt.Remarks = model.Remarks;
            pt.ShiyongTime = model.ShiyongTime;
            pt.ShiyongTime2 = model.ShiyongTime2;
            pt.Yongtu = model.Yongtu;
            pt.ShenhePeople = model.ShenhePeople;

            c.SaveChanges();
            return true;

        }

        public bool DeShigong(Guid id)
        {

            List<ShigongEnclosure> li = c.ShigongEnclosure.Where(w => w.ShigongId == id).ToList();
            for (int i = li.Count - 1; i >= 0; i--)
            {
                c.ShigongEnclosure.Remove(li.ElementAt(i));
            }

            List<ShigongImages> lii = c.ShigongImages.Where(w => w.ShigongId == id).ToList();
            for (int i = lii.Count - 1; i >= 0; i--)
            {
                c.ShigongImages.Remove(lii.ElementAt(i));
            }

            Shigong pt = c.Shigong.FirstOrDefault(n => n.Id == id);
            c.Shigong.Remove(pt);
            c.SaveChanges();
            return true;

        }

        public bool AddShigongImages(ShigongImagesModel model)
        {

            model.Id = Guid.NewGuid();
            ShigongImages pt = GetMapperDTO.SetModel<ShigongImages, ShigongImagesModel>(model);

            c.ShigongImages.Add(pt);
            c.SaveChanges();
            return true;

        }

        public List<ShigongImagesDTO> GetShigongImagesList(Guid id)
        {
            List<ShigongImagesDTO> dtoli = new List<ShigongImagesDTO>();

            List<ShigongImages> li = c.ShigongImages.Where(w => w.ShigongId == id).ToList();

            dtoli = GetMapperDTO.GetDTOList<ShigongImages, ShigongImagesDTO>(li);

            return dtoli;
        }

        public bool DeShigongImages(Guid id)
        {

            ShigongImages pt = c.ShigongImages.FirstOrDefault(n => n.Id == id);
            c.ShigongImages.Remove(pt);
            c.SaveChanges();
            return true;

        }

        public bool AddShigongEnclosure(ShigongEnclosureModel model)
        {

            model.Id = Guid.NewGuid();
            ShigongEnclosure pt = GetMapperDTO.SetModel<ShigongEnclosure, ShigongEnclosureModel>(model);

            c.ShigongEnclosure.Add(pt);
            c.SaveChanges();
            return true;

        }

        public List<ShigongEnclosureDTO> GetShigongEnclosureList(Guid id)
        {
            List<ShigongEnclosureDTO> dtoli = new List<ShigongEnclosureDTO>();

            List<ShigongEnclosure> li = c.ShigongEnclosure.Where(w => w.ShigongId == id).ToList();

            dtoli = GetMapperDTO.GetDTOList<ShigongEnclosure, ShigongEnclosureDTO>(li);

            return dtoli;
        }

        public bool DeShigongEnclosure(Guid id)
        {

            ShigongEnclosure pt = c.ShigongEnclosure.FirstOrDefault(n => n.Id == id);
            c.ShigongEnclosure.Remove(pt);
            c.SaveChanges();
            return true;

        }
    }
}
