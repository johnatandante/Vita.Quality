﻿using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class DefectService : IDefectService
    {
        public IDefect Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<IDefect> GetAllDefects()
        {
            throw new NotImplementedException();
        }

        public List<IDefect> GetMyTasks()
        {
            throw new NotImplementedException();
        }

        public void Save(IDefect model)
        {
            throw new NotImplementedException();
        }
    }
}