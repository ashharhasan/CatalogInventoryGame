using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        public static List<ItemDto> items = new List<ItemDto>{
            new ItemDto(Guid.NewGuid(),"Potion", "Restores a small amount of health", 5,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"SpecialPotion", "Restores huge amount of health", 10,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Bronze Sword", "Deal small damage", 20,DateTimeOffset.UtcNow),
            };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;

        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.SingleOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return items.SingleOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            ItemDto item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }


        [Route("{id}")]
        [HttpPut]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.SingleOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            var updatedItem = existingItem with
            {
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
                Name = updateItemDto.Name
            };

            var index = items.FindIndex(x => x.Id == id);

            items[index] = updatedItem;
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            int index = items.FindIndex(x => x.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            items.RemoveAt(index);
            return NoContent();
        }

    }

}