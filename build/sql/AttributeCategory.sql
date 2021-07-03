CREATE MATERIALIZED VIEW AttributeCategoryMV AS
SELECT
	A."Id" AS "AttributeId", A."Name" AS "AttributeName", A."Title" AS "AttributeTitle",
	C."Id" AS "CategoryId", C."Name" AS "CategoryName", C."Title" AS "CategoryTitle"
FROM public."Attributes" AS A
JOIN
	public."ProductAttributes" AS PA
	ON PA."AttributeId" = A."Id"
JOIN
	public."Products" AS P
	ON PA."ProductId" = P."Id"
JOIN
	public."CategoryProduct" AS CP
	ON CP."ProductsId" = P."Id"
JOIN
	public."Categories" AS C
	ON CP."CategoriesId" = C."Id"
