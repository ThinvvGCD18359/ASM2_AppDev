using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM2.Models
{
	public class Trainee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Education { get; set; }
		public string Experience { get; set; }
		public string Department { get; set; }
		public string Location { get; set; }
		public DateTime Dob { get; set; }
		public string Language { get; set; }
		public int ToeicScore { get; set; }


		public int CourseId { get; set; }
		public Course Course { get; set; }
		public int TopicId { get; set; }
		public Topic Topic { get; set; }
	}
}