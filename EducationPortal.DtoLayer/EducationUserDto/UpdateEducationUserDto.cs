﻿using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.EducationParticipantDto
{
    public class UpdateEducationUserDto
    {
        public int Id { get; set; }
        public int EducationId { get; set; }
        public int ParticipantId { get; set; }
    }
}
