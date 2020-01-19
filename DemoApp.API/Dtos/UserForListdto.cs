using System;

namespace DemoApp.API.Dtos
{
    public class UserForListdto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string   Gender{get;set;}
        public int   Age{get;set;}
        public string   Knownas{get;set;}
        public DateTime   CreatedAt{get;set;}
         public DateTime   LastActive{get;set;}
        public string   City{get;set;}
        public string   Country{get;set;}
        public string PhotoUrl {get;set;}
    }
}