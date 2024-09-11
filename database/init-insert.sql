INSERT INTO public.users
(createdAt, updatedAt, email, password, isAdmin)
VALUES(now(), null, 'igoraujo93@gmail.com', 'senha123', true);

INSERT INTO public.users
(createdAt, updatedAt, email, password, isAdmin)
VALUES(now(), null, 'finn@adventure.time', 'senha123', true);

INSERT INTO public.users
(createdAt, updatedAt, email, password, isAdmin)
VALUES(now(), null, 'jake@adventure.time', 'senha123', false);


INSERT INTO public.admins
(userId, createdAt, updatedAt, createdByUserId, name)
VALUES(1, now(), null, 1, 'Igor Araujo');

INSERT INTO public.admins
(userId, createdAt, updatedAt, createdByUserId, name)
VALUES(2, now(), null, 1, 'Jake');


INSERT INTO public.deliveryPerson
(userId, createdAt, updatedAt, name, cnpj, dateOfBirth, driverLicense, licenseType)
VALUES(4, now(), null, 'Finn', '86581905000118', '2000-09-09 16:43:08.863', '55219750613', 'B');


select * from public.admins

select * from public.users

select * from public.deliveryPerson


-- buscar usuario
SELECT
    a.name, 
    u.email, 
    u.isAdmin,
    (
		SELECT aa.name
	 		FROM public.admins AS aa
	 	WHERE aa.id = a.createdByUserId
    ) AS createdByUser,
    u.createdAt,
    u.updatedAt
FROM
    public.admins AS a
INNER JOIN
    public.users AS u ON u.id = a.userId;
   

 select u.*, dp.* from users as u 
 inner join deliveryPerson as dp on dp.userId = u.id 

 
 INSERT INTO public.motorcycle
(plateNumber, year, model, createdAt, updatedAt)
VALUES('JNK8742', 2024, 'CG Titan', now(), null);


 INSERT INTO public.motorcycle
(plateNumber, year, model, createdAt, updatedAt)
VALUES('HUR4955', 2023, 'CG Titan', now(), null);

select * from public.motorcycle;


INSERT INTO public.product
(createdAt, updatedAt, description, numberOfDays, pricePerDay, lateFee, isPercent)
VALUES(now(), null, '7 dias com um custo de R$30,00 por dia', 7, 30, 20, true);


INSERT INTO public.product
(createdAt, updatedAt, description, numberOfDays, pricePerDay, lateFee, isPercent)
VALUES(now(), null, '15 dias com um custo de R$28,00 por dia', 15, 28, 40, true);

INSERT INTO public.product
(createdAt, updatedAt, description, numberOfDays, pricePerDay, lateFee, isPercent)
VALUES(now(), null, '30 dias com um custo de R$30,00 por dia', 30, 22, 50, false);

INSERT INTO public.product
(createdAt, updatedAt, description, numberOfDays, pricePerDay, lateFee, isPercent)
VALUES(now(), null, '45 dias com um custo de R$30,00 por dia', 45, 20, 50, false);

INSERT INTO public.product
(createdAt, updatedAt, description, numberOfDays, pricePerDay, lateFee, isPercent)
VALUES(now(), null, '50 dias com um custo de R$30,00 por dia', 50, 18, 50, false);

select * from public.product;



