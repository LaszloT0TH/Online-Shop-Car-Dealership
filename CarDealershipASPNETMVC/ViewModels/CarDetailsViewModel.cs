﻿using CarDealershipASPNETMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealershipASPNETMVC.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarModel Car { get; set; }

        [NotMapped]
        public string? EncryptedId { get; set; }

        public IFormFile? Photo { get; set; }

    }
}
