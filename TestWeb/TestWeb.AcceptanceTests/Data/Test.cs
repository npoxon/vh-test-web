﻿using System.Collections.Generic;
using TestWeb.TestApi.Client;

namespace TestWeb.AcceptanceTests.Data
{
    public class Test
    {
        public string AllocateUsername { get; set; }
        public List<string> CaseNames { get; set; }
        public int Endpoints { get; set; }
        public List<User> Users { get; set; }
        public ConferenceDetailsResponse Conference { get; set; }
    }
}