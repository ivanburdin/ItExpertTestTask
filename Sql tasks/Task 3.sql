CREATE TABLE "Dates"
(
    "Id" bigint,
    "Dt" date
);

insert into "Dates" ("Id", "Dt")
values (1, TO_DATE('01.01.2021', 'DD.MM.YYYY')),
       (1, TO_DATE('10.01.2021', 'DD.MM.YYYY')),
       (1, TO_DATE('30.01.2021', 'DD.MM.YYYY')),

       (2, TO_DATE('15.01.2021', 'DD.MM.YYYY')),
       (2, TO_DATE('30.01.2021', 'DD.MM.YYYY'))
;

-- Написать запрос, который возвращает интервалы для одинаковых Id.

select *
from (select "Id",
             "Dt"                                as "Sd",
             lead("Dt") over (partition by "Id") as "Ed"
      from "Dates") s
where "Ed" is not null;