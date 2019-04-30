-----------GETS THE AMOUNT OF WINS A TEAM HAS------------------------------------
SELECT 	
	(SELECT COUNT(*)
		FROM (
		SELECT TEAM_API_ID 
			FROM TEAM WHERE TEAM_LONG_NAME = 'INSERTTEAM') AS HOME_TEAM
		JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID
		WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) +		
		(SELECT COUNT(*)
		FROM (
			SELECT TEAM_API_ID 
				FROM TEAM WHERE TEAM_LONG_NAME = 'INSERTTEAM') AS AWAY_TEAM
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
				FROM TEAM WHERE TEAM_LONG_NAME = 'INSERT') AS HOME_TEAM
				JOIN MATCH ON MATCH.HOME_TEAM_API_ID = HOME_TEAM.TEAM_API_ID
				WHERE MATCH.HOME_TEAM_GOAL > MATCH.AWAY_TEAM_GOAL) +		
				(SELECT COUNT(*)
					FROM (
					SELECT TEAM_API_ID 
					FROM TEAM WHERE TEAM_LONG_NAME = 'INSERT') AS AWAY_TEAM
					JOIN MATCH ON MATCH.AWAY_TEAM_API_ID = AWAY_TEAM.TEAM_API_ID
					WHERE MATCH.HOME_TEAM_GOAL < MATCH.AWAY_TEAM_GOAL)
					AS SumCount)
		ORDER by team_long_name		
---------------------------------------------------------------------------------------------------		