﻿using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowAcions { get; set; }
        public string Heading { get; set; }
    }
}