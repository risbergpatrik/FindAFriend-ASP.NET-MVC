﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindAFriend.Controllers
{
	[Authorize]
	public class AuthenticationController : Controller { }

}