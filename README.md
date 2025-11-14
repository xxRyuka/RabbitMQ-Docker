RabbitMQ'yu dockerda ayağa kaldrmak için 
sudo docker run -d \
  --name rabbitmq \
  --hostname my-rabbit \
  --network rabbit-net \
  -p 5672:5672 \ => Dockerin kendi networkunda izole olarak calısan 5672 portundaki RabbitMQ servisini bilgisayarımızın 5672 portuyla eşleşstiriyoruz  
  -p 15672:15672 \ => RabbitMQ Admin management clientini bilgisayarımızın 15672 portuna baglıyoruz
  -e RABBITMQ_DEFAULT_USER=ryuka \
  -e RABBITMQ_DEFAULT_PASS=123 \
  rabbitmq:3-management

  -v(volume) kullanmadım geçici olarak deneysel çalıstıgımız için  

