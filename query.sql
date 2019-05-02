-----------GETS THE PLAYERS With names like X ('B' in this case)-----------------
Select PLAYER_NAME as Name 
	FROM player
	WHERE Name LIKE  'B%';

-----------GETS THE PLAYERS WITH RANK HIGHER THAN X (48 in this case)------------
Select P.player_name AS Name, Player_Attributes.overall_rating AS Rating 
	FROM Player AS P 
	JOIN Player_Attributes ON P.player_api_id =  Player_Attributes.player_api_id 
	Group BY p.player_api_id Having Player_Attributes.overall_rating >= 48 
	Order By Rating;

-----------GETS THE AMOUNT OF WINS A TEAM HAS------------------------------------
SELECT 	
	(SELECT COUNT(*)
		FROM (
		SELECT TEAM_API_ID 
			FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS HOME_TEAM
		JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID
		WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) +		
		(SELECT COUNT(*)
		FROM (
			SELECT TEAM_API_ID 
				FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS AWAY_TEAM
		JOIN MATCH ON MATCH.AWAY_TEAM_API_ID = AWAY_TEAM.TEAM_API_ID
		WHERE MATCH.HOME_TEAM_GOAL < MATCH.AWAY_TEAM_GOAL)
		AS SumCount;
-----------------------------------------------------------------------------------------
---------------------------------COMPARES TWO TEAMS WINS AGAINST EACHOTHER---------------------------------
SELECT count(*) AS COUNTS, team_long_name
FROM (
	SELECT A.team_long_name
		FROM(
		SELECT C.team_long_name, C.home_team_goal, C.away_team_goal	
			FROM 
			(SELECT MATCH.home_team_goal,MATCH.away_team_goal, HOME_TEAM.team_long_name
				FROM TEAM AS HOME_TEAM
				JOIN MATCH ON MATCH.home_team_api_id = HOME_TEAM.team_api_id
				) AS C
				WHERE C.home_team_goal > C.away_team_goal ) AS A		
	UNION ALL
		SELECT A.team_long_name		
			FROM 
			(SELECT C.team_long_name, C.home_team_goal, C.away_team_goal	
				FROM 
				(SELECT MATCH.home_team_goal,MATCH.away_team_goal, away_TEAM.team_long_name
					FROM TEAM AS AWAY_TEAM
					JOIN MATCH ON MATCH.AWAY_team_api_id = AWAY_TEAM.team_api_id
					) AS C
					WHERE C.home_team_goal < C.away_team_goal ) AS A
		)
		GROUP BY team_long_name
		HAVING COUNTS > 
		(SELECT 	
			(SELECT COUNT(*)
				FROM (
				SELECT TEAM_API_ID 
				FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS HOME_TEAM
				JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID
				WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) +		
				(SELECT COUNT(*)
					FROM (
					SELECT TEAM_API_ID 
					FROM TEAM WHERE TEAM_LONG_NAME = '" + row[0] + "') AS AWAY_TEAM
					JOIN MATCH ON MATCH.AWAY_TEAM_API_ID = AWAY_TEAM.TEAM_API_ID
					WHERE MATCH.HOME_TEAM_GOAL < MATCH.AWAY_TEAM_GOAL)
					AS SumCount)
		ORDER by team_long_name		
---------------------------------------------------------------------------------------------------	
--------------- GET WHICH COUNTRYS ARE IN WHICH League --------------------------------
SELECT Country.NAME AS CountryName, League.name AS LeagueName
	FROM Country
	JOIN League ON League.country_id = Country.id;
--------------------------Give betting deatils about teams ----------------------------
SELECT team_long_name, 
team_short_name,
buildUpPlaySpeed,
buildUpPlaySpeedClass,
buildUpPlayDribbling,
buildUpPlayDribblingClass,
buildUpPlayPassing,
buildUpPlayPassingClass,
buildUpPlayPositioningClass,
chanceCreationPassing,
chanceCreationPassingClass,
chanceCreationCrossing,
chanceCreationCrossingClass,
chanceCreationShooting,
chanceCreationShootingClass,
chanceCreationPositioningClass,
defencePressure,
defencePressureClass,
defenceAggression,
defenceAggressionClass,
defenceTeamWidth,
defenceTeamWidthClass,
defenceDefenderLineClass
	FROM TEAM 
	JOIN Team_Attributes ON Team.team_api_id = Team_Attributes.team_api_id;
------------------------------------------------------------------------------------------------------------