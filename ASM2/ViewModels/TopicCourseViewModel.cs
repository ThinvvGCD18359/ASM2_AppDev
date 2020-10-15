using ASM2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM2.ViewModels
{
	public class TopicCourseViewModel
	{
		public Topic Topic { get; set; }
		public IEnumerable<Course> Courses { get; set; }
	}
}