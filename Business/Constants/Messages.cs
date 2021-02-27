using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi Geçersiz";
        public static string MaintenanceTime = "Sunucu Bakımda";
        public static string ProductsListed = "ürünler listelendi";
        public static string AuthorizationDenied = "yetkiniz yok";
        public static string UserRegistered = "kayıt yapıldı";
        public static string UserNotFound = "kullanıcı bulunamadı";
        public static string PasswordError = "sifre hatalı";
        public static string SuccessfulLogin = "giriş yapıldı";
        public static string UserAlreadyExists = "çıkış yapıldı";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
