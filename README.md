

#Foriba Bulut API Test Projesi

Bu proje e-Fatura, e-Arşiv ve e-İrsaliye Foriba Bulut API web servis metodlarının nasıl kullanılması gerektiği ile ilgili örnek olması için oluşturulmuştur. 
Projede üç farklı ürünün web servis metodlarının kullanımı ve açıklamaları her metod için bulunmaktadır. Yalnızca test sisteminde çalışmakta ve web servislere bağlantı ayarları da projede bulunmaktadır.

 **e-Fatura Ürünü İçin:**

-Mükellef Sorgulama
-Fatura ve Uygulama Yanıtı Gönderme
-Gönderilen Zarfların Durumunu Sorgulama
-Gelen veya Gönderilen Faturaların HTML,PDF,UBL Belgelerini İndirme

**e-Arşiv Ürünü İçin:**

-Fatura Gönderme
-Gönderilen e-Arşiv Faturaların HTML,PDF,UBL Belgelerini İndirme

**e-İrsaliye Ürünü İçin:**

-Mükellef Sorgulama
-İrsaliye ve İrsaliye Yanıtı Gönderme
-Gönderilen Zarfların Durumunu Sorgulama
-Gelen veya Gönderilen İrsaliye ve İrsaliye Yanıtlarının HTML,PDF,UBL Belgelerini İndirme

işlemleri yapılmaktadır.


# Kurulum

Bu proje Visual Studio 2015 .Net Framework 4.7 ortamında oluşturulmuştur.

Foriba.OE.CLIENT Projesi Altında Bulunan Servislerin Kurulumu:

-Referans eklemek istenilen projenin References bölümü üzerinde sağ tıklayıp Add Service Reference  veya Visual Studio menüleri üzerinden 
Project-> Add Service Reference takip ederek Servis ekleme ekranına gelinir. 
-Servis ekleme ekranında zip dosyasından çıkartılan WSDL dokümanının dizini belirtilir ve GO butonuna basılır. 
-Sonrasında ekranda WSDL dosyasında bulunan Servis ve Servise ait metodların bir listesi çıkmalıdır.
-Bu metodların kullanılacağı Class’ları içerecek olan Proxy isimlendirmesi NameSpace bölümünden yapılabilir.
-Namespace ServiceReference1 olarak otomatik gelmektedir.(ServiceReference1 alanı farklı bir namespace kullanılırak isteğe bağlı değiştirilebilir.) 
-OK Butonuna basıldığında projeye Servis Referansı eklenmiş olur. 
-Bu adımdan sonra web servis metodlarına ServiceReference1 namespace’i kullanılarak erişilebilir.


# Lisans
  
Foriba Bulut API Test Projesi, **Foriba R&D** ekibi tarafından API kullanımını anlatmak için hazırlanmıştır, izinsiz olarak ticari uygulamalarda kullanılması yasaktır.  
