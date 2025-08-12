use projeto

select * from [calendarbar.[cat]

insert into [calendarbar.[cat] (id_calendarbar,type,dateMin,dateMax,fiscalYearStart) values (1,'calendarBar','01/09/2006','31/12/2012','01/07');

select * from calendarbar.itemsbar_cat

insert into calendarbar.itemsbar_cat(id_itembar,description) values(6,'fiscalYear');

select * from calendarbar.itemsbar_rel

insert into calendarbar.itemsbar_rel (id_calendarbar,id_itembar, visible,range,rangeStart,rangeEnd) values(1,6,1,1,'','');

select * from calendarbar.defaultselections_cat

insert into calendarbar.defaultselections_cat(id_defaultselection,selection,range,dateStart,dateEnd) values(1, 'day', 0, '09/12/2012','');

select * from calendarbar.defaultselection_rel

insert into calendarbar.defaultselection_rel (id_calendarbar,id_defaultselection) values(1,1);

ALTER TABLE calendarbar.defaultselections_cat
ALTER COLUMN range BIT;

SELECT
  c.type AS [type],

  JSON_QUERY( -- todo calendarBar
    (
      SELECT
        JSON_QUERY( -- datetime
          (SELECT ir.visible, ir.range, ir.rangeStart, ir.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS datetime,

        JSON_QUERY( -- day
          (SELECT ir2.visible, ir2.range, ir2.rangeStart, ir2.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS day,

        JSON_QUERY( -- week
          (SELECT ir3.visible, ir3.range, ir3.rangeStart, ir3.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS week,

        JSON_QUERY( -- month
          (SELECT ir4.visible, ir4.range, ir4.rangeStart, ir4.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS month,

        JSON_QUERY( -- year
          (SELECT ir5.visible, ir5.range, ir5.rangeStart, ir5.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS year,

        JSON_QUERY( -- fiscalYear
          (SELECT ir6.visible, ir6.range, ir6.rangeStart, ir6.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS fiscalYear,

        JSON_QUERY( -- defaultSelection
          (SELECT ds.selection, ds.range, ds.dateStart, ds.dateEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS defaultSelection
      FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    )
  ) AS calendarBar,

  JSON_QUERY('{
    "descriptions": ["Mañana", "Tarde", "Noche", "Todos"],
    "visible": true,
    "range": false,
    "rangeStart": null,
    "rangeEnd": null,
    "typeCtrl": "combobox"
  }') AS shiftBar,

  c.dateMin,
  c.dateMax,
  c.fiscalYearStart

FROM [calendarbar.[cat] c
CROSS APPLY (
  SELECT ir.visible, ir.range, ir.rangeStart, ir.rangeEnd
  FROM calendarbar.itemsbar_rel ir
  INNER JOIN calendarbar.itemsbar_cat ic ON ir.id_itembar = ic.id_itembar
  WHERE ir.id_calendarbar = c.id_calendarbar AND ic.description = 'datetime'
) AS ir

CROSS APPLY (
  SELECT ir2.visible, ir2.range, ir2.rangeStart, ir2.rangeEnd
  FROM calendarbar.itemsbar_rel ir2
  INNER JOIN calendarbar.itemsbar_cat ic2 ON ir2.id_itembar = ic2.id_itembar
  WHERE ir2.id_calendarbar = c.id_calendarbar AND ic2.description = 'day'
) AS ir2

CROSS APPLY (
  SELECT ir3.visible, ir3.range, ir3.rangeStart, ir3.rangeEnd
  FROM calendarbar.itemsbar_rel ir3
  INNER JOIN calendarbar.itemsbar_cat ic3 ON ir3.id_itembar = ic3.id_itembar
  WHERE ir3.id_calendarbar = c.id_calendarbar AND ic3.description = 'week'
) AS ir3

CROSS APPLY (
  SELECT ir4.visible, ir4.range, ir4.rangeStart, ir4.rangeEnd
  FROM calendarbar.itemsbar_rel ir4
  INNER JOIN calendarbar.itemsbar_cat ic4 ON ir4.id_itembar = ic4.id_itembar
  WHERE ir4.id_calendarbar = c.id_calendarbar AND ic4.description = 'month'
) AS ir4

CROSS APPLY (
  SELECT ir5.visible, ir5.range, ir5.rangeStart, ir5.rangeEnd
  FROM calendarbar.itemsbar_rel ir5
  INNER JOIN calendarbar.itemsbar_cat ic5 ON ir5.id_itembar = ic5.id_itembar
  WHERE ir5.id_calendarbar = c.id_calendarbar AND ic5.description = 'year'
) AS ir5

CROSS APPLY (
  SELECT ir6.visible, ir6.range, ir6.rangeStart, ir6.rangeEnd
  FROM calendarbar.itemsbar_rel ir6
  INNER JOIN calendarbar.itemsbar_cat ic6 ON ir6.id_itembar = ic6.id_itembar
  WHERE ir6.id_calendarbar = c.id_calendarbar AND ic6.description = 'fiscalYear'
) AS ir6

LEFT JOIN calendarbar.defaultselection_rel dr ON dr.id_calendarbar = c.id_calendarbar
LEFT JOIN calendarbar.defaultselections_cat ds ON dr.id_defaultselection = ds.id_defaultselection

WHERE c.id_calendarbar = 1

FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;


create procedure GetCalendario
 as 
 BEGIN
  SELECT
  c.type AS [type],

  JSON_QUERY( -- todo calendarBar
    (
      SELECT
        JSON_QUERY( -- datetime
          (SELECT ir.visible, ir.range, ir.rangeStart, ir.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS datetime,

        JSON_QUERY( -- day
          (SELECT ir2.visible, ir2.range, ir2.rangeStart, ir2.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS day,

        JSON_QUERY( -- week
          (SELECT ir3.visible, ir3.range, ir3.rangeStart, ir3.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS week,

        JSON_QUERY( -- month
          (SELECT ir4.visible, ir4.range, ir4.rangeStart, ir4.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS month,

        JSON_QUERY( -- year
          (SELECT ir5.visible, ir5.range, ir5.rangeStart, ir5.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS year,

        JSON_QUERY( -- fiscalYear
          (SELECT ir6.visible, ir6.range, ir6.rangeStart, ir6.rangeEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS fiscalYear,

        JSON_QUERY( -- defaultSelection
          (SELECT ds.selection, ds.range, ds.dateStart, ds.dateEnd
           FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
        ) AS defaultSelection
      FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    )
  ) AS calendarBar,

  JSON_QUERY('{
    "descriptions": ["Mañana", "Tarde", "Noche", "Todos"],
    "visible": true,
    "range": false,
    "rangeStart": null,
    "rangeEnd": null,
    "typeCtrl": "combobox"
  }') AS shiftBar,

  c.dateMin,
  c.dateMax,
  c.fiscalYearStart

FROM [calendarbar.[cat] c
CROSS APPLY (
  SELECT ir.visible, ir.range, ir.rangeStart, ir.rangeEnd
  FROM calendarbar.itemsbar_rel ir
  INNER JOIN calendarbar.itemsbar_cat ic ON ir.id_itembar = ic.id_itembar
  WHERE ir.id_calendarbar = c.id_calendarbar AND ic.description = 'datetime'
) AS ir

CROSS APPLY (
  SELECT ir2.visible, ir2.range, ir2.rangeStart, ir2.rangeEnd
  FROM calendarbar.itemsbar_rel ir2
  INNER JOIN calendarbar.itemsbar_cat ic2 ON ir2.id_itembar = ic2.id_itembar
  WHERE ir2.id_calendarbar = c.id_calendarbar AND ic2.description = 'day'
) AS ir2

CROSS APPLY (
  SELECT ir3.visible, ir3.range, ir3.rangeStart, ir3.rangeEnd
  FROM calendarbar.itemsbar_rel ir3
  INNER JOIN calendarbar.itemsbar_cat ic3 ON ir3.id_itembar = ic3.id_itembar
  WHERE ir3.id_calendarbar = c.id_calendarbar AND ic3.description = 'week'
) AS ir3

CROSS APPLY (
  SELECT ir4.visible, ir4.range, ir4.rangeStart, ir4.rangeEnd
  FROM calendarbar.itemsbar_rel ir4
  INNER JOIN calendarbar.itemsbar_cat ic4 ON ir4.id_itembar = ic4.id_itembar
  WHERE ir4.id_calendarbar = c.id_calendarbar AND ic4.description = 'month'
) AS ir4

CROSS APPLY (
  SELECT ir5.visible, ir5.range, ir5.rangeStart, ir5.rangeEnd
  FROM calendarbar.itemsbar_rel ir5
  INNER JOIN calendarbar.itemsbar_cat ic5 ON ir5.id_itembar = ic5.id_itembar
  WHERE ir5.id_calendarbar = c.id_calendarbar AND ic5.description = 'year'
) AS ir5

CROSS APPLY (
  SELECT ir6.visible, ir6.range, ir6.rangeStart, ir6.rangeEnd
  FROM calendarbar.itemsbar_rel ir6
  INNER JOIN calendarbar.itemsbar_cat ic6 ON ir6.id_itembar = ic6.id_itembar
  WHERE ir6.id_calendarbar = c.id_calendarbar AND ic6.description = 'fiscalYear'
) AS ir6

LEFT JOIN calendarbar.defaultselection_rel dr ON dr.id_calendarbar = c.id_calendarbar
LEFT JOIN calendarbar.defaultselections_cat ds ON dr.id_defaultselection = ds.id_defaultselection

WHERE c.id_calendarbar = 1

FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
 END

 SHOW CREATE PROCEDURE GetCalendario;

 EXEC sp_helptext 'GetCalendario';

 exec GetCalendario;

 exec GetCalendario; 