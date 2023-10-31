CREATE OR ALTER PROCEDURE uspReporteReserva
@Desde DATE,
@Hasta DATE
AS
BEGIN
SELECT T.Nombre AS TITULO, R.TotalVentas, E.PromedioClasificacion
    FROM GamerShop.Titulo T
    LEFT JOIN (
        SELECT R.TituloId, SUM(R.ImporteTotal) AS TotalVentas
        FROM GamerShop.Reserva R
        WHERE @Hasta >= R.FechaTxn AND @Desde <= R.FechaTxn
        GROUP BY R.TituloId
    ) R ON T.Id = R.TituloId
    LEFT JOIN (
        SELECT E.TituloId, AVG(E.Calificacion) AS PromedioClasificacion
        FROM GamerShop.Evaluacion E
		WHERE @Hasta >= E.Fecha AND @Desde <= E.Fecha
        GROUP BY E.TituloId
    ) E ON T.Id = E.TituloId;
--SELECT T.Nombre, SUM(R.ImporteTotal) AS TotalVentas, AVG(E.Calificacion) AS PromedioClasificacion
--FROM GamerShop.Reserva R
--INNER JOIN GamerShop.Titulo T
--ON T.Id = R.TituloId
--INNER JOIN GamerShop.Evaluacion E
--ON T.Id = E.TituloId
--WHERE @Hasta >= R.FechaTxn AND @Desde <= R.FechaTxn
--GROUP BY T.Nombre
END