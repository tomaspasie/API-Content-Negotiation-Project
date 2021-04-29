using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using System;
using System.Collections.Generic;

namespace SchoolAPI.Controllers
{
    public abstract class CommonProperties : ControllerBase
    {
        public abstract IActionResult Get();

        public abstract IActionResult Get(Guid id);

        public abstract IActionResult Create([FromBody] CreateItem item);

        public abstract IActionResult Update(Guid id, [FromBody] NameString str);

        public abstract IActionResult Delete(Guid id);

    }
}
