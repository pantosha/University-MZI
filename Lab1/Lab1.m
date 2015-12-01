text = 'abc';
encryption = 'def';
assert(strcmp(caesarEncrypt(text, 3), encryption));
assert(strcmp(caesarEncrypt(encryption, -3), text));

text = 'For National Road 5, which runs north-south between the towns of Maroantsetra (pictured here) and Soanierana-Ivongo on the African country’s east coast, “you need to hire both a driver and a mechanic,” said Anders Alm, chief technology officer for WAU, a travel agency that provides regular trips to the area. If you’re “bored of concrete”, he added, this drive – which he called “the worst road in the world” – would be one way to change it up. With sections of sand, solid rock and even worn-down bridges that drivers must inspect before crossing, the 200km road takes nearly 24 hours to drive. It turns especially treacherous during the rainy season (December to March), when the lack of asphalt or concrete paving leads the road to become impassable in many spots. The upside? Most of National Road 5 runs along the white sand coastline, providing spectacular views of palm tree forests and the Indian Ocean.';
[encryption, decryptedText] = caesarEncrypt(text, 5);
assert(strcmp(caesarDecrypt(encryption), decryptedText));

disp('It''s work!');