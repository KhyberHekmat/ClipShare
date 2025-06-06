﻿using ClipShare.Core.Entities;
using ClipShare.Core.IRepo;
using ClipShare.Extensions;
using ClipShare.Utility;
using ClipShare.ViewModels.Channel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace ClipShare.Controllers
{
    [Authorize(Roles =$"{SD.UserRole}")]
    public class ChannelController : CoreController
    {
     
        public async Task<IActionResult> Index(string stringModel)
        {
            var model = new ChannelAddEdit_vm();
            stringModel = HttpContext.Session.GetString("ChannelModelFromSession");

            if (!string.IsNullOrEmpty(stringModel))
            {
                model = JsonConvert.DeserializeObject<ChannelAddEdit_vm>(stringModel);

                if (model.Errors.Count > 0)
                {
                    foreach(var error in model.Errors)
                    {
                        ModelState.AddModelError(error.Key, error.ErrorMessage);
                    }

                    HttpContext.Session.Remove("ChannelModelFromSession");

                    return View(model);
                }
            }

            var channel = await UnitOfWork.ChannelRepo.GetFirstOrDefaultAsync(x => x.AppUserId == User.GetUserId());

            if(channel != null)
            {
                model.Name = channel.Name;
                model.About = channel.About;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(ChannelAddEdit_vm model)
        {
            if (!ModelState.IsValid)
            {
                foreach(var item in ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        model.Errors.Add(new ModelError_vm
                        {
                            Key = item.Key,
                            ErrorMessage = item.Value.Errors.Select(x => x.ErrorMessage).FirstOrDefault()
                        });
                    }
                }

                HttpContext.Session.SetString("ChannelModelFromSession", JsonConvert.SerializeObject(model));

                return RedirectToAction("Index");

            }
            //To check the duplicat channel 
            var channelNameExists = await UnitOfWork.ChannelRepo.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (channelNameExists)
            {
                model.Errors.Add(new ModelError_vm
                {
                    Key = "Name",
                    ErrorMessage = $"Channel name of {model.Name} is taken. Please try other name"
                });

                HttpContext.Session.SetString("ChannelModelFromSession", JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }

            var channelToAdd = new Channel
            {
                AppUserId = User.GetUserId(),
                Name = model.Name,
                About = model.About
            };

            UnitOfWork.ChannelRepo.Add(channelToAdd);
            await UnitOfWork.CompleteAsync();

            TempData["notification"] = "true;Channel Created;Your channel has been created and you can upload clips now";

            return RedirectToAction("Index");
        }
    }
}
