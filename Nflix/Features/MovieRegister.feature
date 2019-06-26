Feature: Movie Register
	As a catalog manager
	I can register a new movie

@login
Scenario Outline: New Movie
	Given a code <movie_code> of a new movie
	When the manager register this movie
	Then the movie should be seen on the catalog

	Examples:
		| movie_code |
		| "spider"   |
		| "ultimato" |