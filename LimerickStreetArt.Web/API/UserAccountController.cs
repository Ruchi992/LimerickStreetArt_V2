namespace LimerickStreetArt.Web.API
{
	using LimerickStreetArt.Repository;
	using LimerickStreetArt.Web.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserAccountController : ControllerBase
	{


		private UserAccountRepository UserAccountRepository { get; }

		public UserAccountController(UserAccountRepository userAccountRepository)

		{
			UserAccountRepository = userAccountRepository;
		}
		
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<UserAccount>> GetUserAccounts()
		{
			return Ok(UserAccountRepository.GetUserAccounts());
		}

		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<UserAccount> GetAction(int id)
		{
			UserAccount userAccount = UserAccountRepository.GetById(id);

			if (userAccount == null)
				return NotFound();

			return Ok(userAccount);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<UserAccount> Create([FromBody]UserAccount item)
		{
			UserAccountRepository.Create(item);
			return CreatedAtAction(nameof(GetUserAccounts), new { item.Id }, item);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult Put(Int32 id, [FromBody] UserAccount userAccount)
		{
			if (id != userAccount.Id)
			{
				return BadRequest();
			}
			try
			{
				UserAccountRepository.Update(userAccount);
			}
			catch (Exception)
			{
				return BadRequest("Error while editing item");
			}
			return NoContent();
		}

		
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<UserAccount> Delete(int id)
		{
			UserAccount userAccount = UserAccountRepository.GetById(id);
			if (userAccount == null)
				return NotFound();
			UserAccountRepository.Delete(userAccount);
			return Ok(userAccount);
		}

		[HttpPost]
		
		
		public ActionResult<bool> Register([FromBody]RegistrationModel registrationModel)
		{
			if (ModelState.IsValid)
			{
				bool passwordsDoNotMatch = registrationModel.Password != registrationModel.ReconformPassword;
				bool usernameIsTaken = false;
				bool emailIsInUse = false;

				if (usernameIsTaken)
				{
					ModelState.AddModelError("", "Usernme is already In use.");
				}
				if (emailIsInUse)
				{
					ModelState.AddModelError("", "e-mail is already In use.");
				}
				if (passwordsDoNotMatch)
				{
					ModelState.AddModelError("", "Passwords don't match.");
				}

				if (!usernameIsTaken && !emailIsInUse && !passwordsDoNotMatch)
				{
					var userAccount = new UserAccount
					{
						AccessLevel = 2,
						Active = true,
						DateOfBirth = registrationModel.DateOfBirth,
						Email = registrationModel.Email,
						Password = registrationModel.Password,
						Username = registrationModel.Username,
					};
					UserAccountRepository.Create(userAccount);
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
			//			return CreatedAtAction(nameof(Get), new { item.Id }, item);
		}

		[HttpPost]
		public ActionResult<UserAccount> Login([FromBody]User user)
		{
			try
			{
				UserAccount userAccount = UserAccountRepository.GetUserAccountByCredentials(user.Username, user.Password);

				if (userAccount == null)
					return NotFound();

				return Ok(userAccount);
			}
			catch (Exception e)
			{

				throw;
			}
		}
		public ActionResult<UserAccount> Login2(string username,string password)
		{
			UserAccount userAccount = UserAccountRepository.GetUserAccountByCredentials(username, password);

			if (userAccount == null)
				return NotFound();

			return Ok(userAccount);
		}
	}


	public class User
	{

		// public int Id { get; set; }
		
		public string Username { get; set; }

		public string Password { get; set; }


		public User() { }

		public User(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public bool CheckInformation()
		{
			if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
				return false;

			return true;
		}
	}
}

