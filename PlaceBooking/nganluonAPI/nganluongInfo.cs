﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceBooking.nganluonAPI
{
    public class nganluongInfo
    {
        public static readonly string Merchant_id = "49934"; // mã Merchant
        public static readonly string Merchant_password = "cb1819203ec6b4c46125cc9840b5e38e";  //Merchant password
        public static readonly string Receiver_email = "nguyencong181995@gmail.com";// email nhan tien
        public static readonly string UrlNganLuong = "https://sandbox.nganluong.vn:8088/nl35/checkout.api.nganluong.post.php";
        // dường dẫn khi thanh tán thành công
        public static readonly string return_url = "http://localhost:54015/confirm-orderPaymentOnline";
        // dường dẫn khi thanh tán thất bại
        public static readonly string cancel_url = "http://localhost:54015/cancel-order";

    }
}