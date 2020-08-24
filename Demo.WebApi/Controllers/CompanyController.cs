using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.Domian.DM;
using Demo.DTO;
using Demo.DTO.CompanyDTO;
using Demo.IDomian.IDM;
using Demo.WebApi.Controllers.Base; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utility;

namespace Demo.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CompanyController : ApiControllerBase
    {

        private readonly ICompanyDM  dm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public CompanyController(ICompanyDM DM)
        {
            dm = DM;
        }

        #region 施工单位
        /// <summary>
        /// 获取施工单位列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShigongList")]
        public ResultEntity<List<ShigongDTO>> GetShigongList([FromQuery]Request_Shigong dto)
        {
            if (ModelState.IsValid)
            {

                LogUtil.log.Info(ModelState);
            }
            else
            {

                LogUtil.log.Info(ModelState);
            }
            return new ResultEntityUtil<List<ShigongDTO>>().Success(dm.GetShigongList(dto, out int count), count);

        }

        /// <summary>
        /// 添加施工单位
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddShigong")]
        public ResultEntity<bool> AddShigong([FromQuery]ShigongModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddShigong(model));

        }
        /// <summary>
        /// 修改施工单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("UpShigong")]
        public ResultEntity<bool> UpShigong([FromQuery]ShigongModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.UpShigong(model));

        }


        /// <summary>
        /// 删除施工单位
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeShigong")]
        public ResultEntity<bool> DeShigong(Guid Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeShigong(Id));

        }
        #endregion

        #region 施工单位图片
        //图片上传路径
        private readonly string uploadPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ShigongImages/";
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns>成功返回图片URL</returns>
        //[HttpPost("UpImages")]
        //public ResultEntity<string> UpImages()
        //{
        //    ResultEntity<string> result = new ResultEntity<string>();
        //    try
        //    {
        //        result.IsSuccess = false;
        //        string imgPath = string.Empty;

        //        HttpRequest request = System.Web.HttpContext.Current.Request;
        //        HttpFileCollection fileCollection = request.Files;

        //        // 判断是否有文件
        //        if (fileCollection.Count > 0)
        //        {
        //            // 获取图片文件
        //            HttpPostedFile httpPostedFile = fileCollection[0];

        //            //文件大小
        //            int fileSize = httpPostedFile.ContentLength;
        //            if (fileSize > 8 * 1024 * 1024)
        //            {
        //                result.Msg = "上传错误，图片大小不能超过1M";
        //                return result;
        //            }
        //            // 文件扩展名
        //            string fileExtension = Path.GetExtension(httpPostedFile.FileName);
        //            // 图片名称
        //            string fileName = Guid.NewGuid().ToString() + fileExtension;
        //            // 图片上传路径
        //            string filePath = uploadPath + fileName;
        //            //httpPostedFile.FileName;

        //            // 验证图片格式
        //            if (fileExtension.Contains(".jpg") || fileExtension.Contains(".png"))
        //            {
        //                // 如果目录不存在则要先创建
        //                if (!Directory.Exists(uploadPath))
        //                {
        //                    Directory.CreateDirectory(uploadPath);
        //                }

        //                // 保存新的图片文件
        //                while (File.Exists(filePath))
        //                {
        //                    fileName = Guid.NewGuid().ToString() + fileExtension;
        //                    filePath = uploadPath + fileName;
        //                }
        //                httpPostedFile.SaveAs(filePath);

        //                int a = filePath.Length;
        //                // 返回图片URL
        //                result.Data = "ProductImages/" + fileName;
        //                result.Msg = "图片上传成功";
        //                result.IsSuccess = true;
        //            }
        //            else
        //            {
        //                result.Msg = "请选择jpg/png/bmp格式的图片";
        //            }
        //        }
        //        else
        //        {
        //            result.Msg = "请先选择图片！";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.IsSuccess = false;
        //        result.Msg = e.Message.ToString();
        //    }
        //    return result;
        //}



        [HttpPost]
        public ResultEntity<string> Upload()//IFormCollection Files
        {
            ResultEntity<string> result = new ResultEntity<string>();

            try
            {
                var form = Request.Form;//直接从表单里面获取文件名不需要参数
                //string dd = Files["File"];
                //var form = Files;//定义接收类型的参数
                string hash = "";
                IFormFileCollection cols = Request.Form.Files;
                if (cols == null || cols.Count == 0)
                {
                    result.Msg = "没有上传文件";
                    return result;
                }
                foreach (IFormFile file in cols)
                {
                    //定义图片数组后缀格式
                    string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                    //获取图片后缀是否存在数组中
                    string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                    if (LimitPictureType.Contains(currentPictureExtension))
                    {

                        //为了查看图片就不在重新生成文件名称了
                        // var new_path = DateTime.Now.ToString("yyyyMMdd")+ file.FileName;
                        var new_path = Path.Combine(uploadPath, file.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", new_path);

                        using (var stream = new FileStream(path, FileMode.Create))
                        { 
                            //图片路径保存到数据库里面去
                            bool flage = true;
                            if (flage == true)
                            {
                                //再把文件保存的文件夹中
                                file.CopyTo(stream);
                                hash = path + new_path;
                            }
                        }

                    }
                    else
                    {
                        result.Msg = "请上传指定格式的图片";
                        return result;

                    }
                }
                // 返回图片URL
                result.Data = hash;//"ProductImages/" + fileName;
                result.Msg = "图片上传成功";
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                return result;
            }

        }

        /// <summary>
        /// 添加施工单位图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddShigongImages")]
        public ResultEntity<bool> AddShigongImages([FromQuery]ShigongImagesModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddShigongImages(model));
        }

        /// <summary>
        /// 获取施工单位图片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShigongImagesList")]
        public ResultEntity<List<ShigongImagesDTO>> GetShigongImagesList(Guid id)
        {
            return new ResultEntityUtil<List<ShigongImagesDTO>>().Success(dm.GetShigongImagesList(id));

        }
        /// <summary>
        /// 删除施工单位图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeShigongImages")]
        public ResultEntity<bool> DeShigongImages(Guid Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeShigongImages(Id));

        }
        #endregion

        #region 施工单位文档

        // 文档上传路径 
        //private readonly string uploadPath2 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ShigongEnclosures/";
        
       
        //[HttpPost("UpShigongEnclosure")]
        //public ResultEntity<string> UpShigongEnclosure()
        //{
        //    ResultEntity<string> result = new ResultEntity<string>();
        //    try
        //    {
        //        result.IsSuccess = false;
        //        string imgPath = string.Empty;

        //        HttpRequest request = System.Web.HttpContext.Current.Request;
        //        HttpFileCollection fileCollection = request.Files;

        //        // 判断是否有文件
        //        if (fileCollection.Count > 0)
        //        {
        //            // 获取图片文件
        //            HttpPostedFile httpPostedFile = fileCollection[0];

        //            //文件大小
        //            int fileSize = httpPostedFile.ContentLength;
        //            if (fileSize > 8 * 1024 * 1024)
        //            {
        //                result.Msg = "上传错误，图片大小不能超过1M";
        //                return result;
        //            }
        //            // 文件扩展名
        //            string fileExtension = Path.GetExtension(httpPostedFile.FileName);
        //            // 图片名称
        //            string fileName = Guid.NewGuid().ToString() + fileExtension;
        //            // 图片上传路径
        //            string filePath = uploadPath2 + fileName;

        //            // 如果目录不存在则要先创建
        //            if (!Directory.Exists(uploadPath2))
        //            {
        //                Directory.CreateDirectory(uploadPath2);
        //            }

        //            // 保存新的图片文件
        //            while (File.Exists(filePath))
        //            {
        //                fileName = Guid.NewGuid().ToString() + fileExtension;
        //                filePath = uploadPath2 + fileName;
        //            }
        //            httpPostedFile.SaveAs(filePath);

        //            int a = filePath.Length;
        //            // 返回图片URL
        //            result.Data = "ShigongEnclosures/" + fileName;
        //            result.Msg = "图片上传成功";
        //            result.IsSuccess = true;

        //        }
        //        else
        //        {
        //            result.Msg = "请先选择图片！";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.IsSuccess = false;
        //        result.Msg = e.Message.ToString();
        //    }
        //    return result;
        //}

        /// <summary>
        /// 添加施工单位图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddShigongEnclosure")]
        public ResultEntity<bool> AddShigongEnclosure([FromQuery]ShigongEnclosureModel model)
        {
            return new ResultEntityUtil<bool>().Success(dm.AddShigongEnclosure(model));
        }

        /// <summary>
        /// 获取施工单位图片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShigongEnclosureList")]
        public ResultEntity<List<ShigongEnclosureDTO>> GetShigongEnclosureList(Guid id)
        {
            return new ResultEntityUtil<List<ShigongEnclosureDTO>>().Success(dm.GetShigongEnclosureList(id));

        }
        /// <summary>
        /// 删除施工单位图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("DeShigongEnclosure")]
        public ResultEntity<bool> DeShigongEnclosure(Guid Id)
        {
            return new ResultEntityUtil<bool>().Success(dm.DeShigongEnclosure(Id));

        }
        #endregion

        #region 监理单位

        #endregion

        #region 建设单位

        #endregion
    }
}