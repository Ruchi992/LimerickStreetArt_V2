namespace LimerickStreetArt.Web.Api
{
    using AutoMapper;
    using LimerickStreetArt;
	using LimerickStreetArt.Repository;
    using LimerickStreetArt.Web.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
	using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;

    [Route("api/[controller]/[action]")]
	[ApiController]
	public class StreetArtController : ControllerBase
	{
		private StreetArtRepository StreetArtRepository { get; }
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;


		public StreetArtController(StreetArtRepository streetArtRepository, IMapper mapper, IWebHostEnvironment env)
		{
			StreetArtRepository = streetArtRepository;
			_mapper = mapper;
			_env = env;
		}
		[HttpGet(Name = nameof(GetStreetArt))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<StreetArt>> GetStreetArt()
		{
			return Ok(StreetArtRepository.GetStreetArtList());
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<StreetArt> GetAction(int id)
		{
			StreetArt streetArt = StreetArtRepository.GetById(id);

			if (streetArt == null)
				return NotFound();

			return Ok(streetArt);
		}

		[HttpGet("[action]/{searchText}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<StreetArt>> Search(String searchText)
		{
			var streetArtList = StreetArtRepository.GetStreetArtList();

			var results = streetArtList.Where(streetArt => streetArt.Title.Contains(searchText));

			return Ok(results);
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<StreetArt> Create([FromBody]StreetArt item)
		{
			StreetArtRepository.Create(item);
			return CreatedAtAction(nameof(GetStreetArt), new { item.Id }, item);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult Put(Int32 id, [FromBody] StreetArt streetArt)
		{
			if (id != streetArt.Id)
			{
				return BadRequest();
			}
			try
			{
				StreetArtRepository.Update(streetArt);
			}
			catch (Exception)
			{
				return BadRequest("Error while editing item");
			}
			return NoContent();
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<StreetArt> Delete(int id)
		{
			StreetArt streetArt = StreetArtRepository.GetById(id);
			if (streetArt == null)
				return NotFound();
			StreetArtRepository.Delete(streetArt);

			string filename = streetArt.Image;

			try
			{
				// Check if file exists with its full path    
				if (System.IO.File.Exists(Path.Combine(_env.WebRootPath + "/pics/", filename)))
				{
					// If file found, delete it    
					System.IO.File.Delete(Path.Combine(_env.WebRootPath + "/pics/", filename));
					
				}
				if (System.IO.File.Exists(Path.Combine(_env.WebRootPath + "/pics/thumbs/",   filename)))
				{
					// If file found, delete it    
					System.IO.File.Delete(Path.Combine(_env.WebRootPath + "/pics/thumbs/", filename));

				}


			}
			catch (IOException ioExp)
			{
				Console.WriteLine(ioExp.Message);
			}
			return Ok(streetArt);
		}

		

		public ActionResult<IEnumerable<StreetArt>> GetStreetArtDTO()
		{
			var StreetArt=(StreetArtRepository.GetStreetArtList());
			List<StreetArtDTO> streetArtDTO = new List<StreetArtDTO>();
			StreetArt.ForEach(art =>
			{
				var tempStreetArtDTO = _mapper.Map<StreetArtDTO>(art);
				tempStreetArtDTO.UserName = StreetArtRepository.GetUserById(art.UserAccountId).Username;
				streetArtDTO.Add(tempStreetArtDTO);
			});
			return Ok(streetArtDTO);

		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		
		public async System.Threading.Tasks.Task<ActionResult<StreetArt>> upload()
		{

			string id =Guid.NewGuid().ToString();
			try
			{
				var httpRequest = HttpContext.Request;
				if (httpRequest.Form.Files.Count > 0)
				{
					foreach (string file in httpRequest.Form.Keys)
					{
						var a = httpRequest.Form[file];
						StreetArt art = JsonConvert.DeserializeObject<StreetArt>(a);
						art.Image = art.Image + "_" + id + ".png";

						StreetArtRepository.Create(art);


					}

					foreach (IFormFile file in httpRequest.Form.Files)
					{
						var filename = file.FileName;
						var filePath = _env.WebRootPath +   "/pics/" + filename + "_" + id+".png";

						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await file.CopyToAsync(fileStream);

							
						}
						Image image = Image.FromFile(filePath);
						Image thumb = image.GetThumbnailImage(60, 60, () => false, IntPtr.Zero);
						thumb.Save(_env.WebRootPath + "/pics/thumbs/" + filename + "_" + id + ".png");

						

					}

					
				}
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}


			return StatusCode(StatusCodes.Status201Created);

			//return CreatedAtAction(nameof(GetStreetArt), new { item.Id }, item);
		}

		
	}
}
