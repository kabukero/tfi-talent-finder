﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
	public class BitacoraFilter
	{
		public Usuario Usuario { get; set; }
		public DateTime? FechaDesde { get; set; }
		public DateTime? FechaHasta { get; set; }
	}
}
