﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class AspNetUserToken : IdentityUserToken<Guid>
{
}
