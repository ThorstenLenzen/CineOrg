export interface Movie {
  id: string;
  title: string;
  genre: string;
  yearReleased: number;
  description: string;
}

export interface MovieForCreate {
  title: string;
  genre: string;
  yearReleased: number;
  description: string;
}

export interface MovieForUpdate {
  title: string;
  genre: string;
  yearReleased: number;
  description: string;
}
