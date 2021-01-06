
  export interface Opstine {
     text: string;
     value: string;
  }

  export interface StudentDodajVM {
    opstine: Opstine[];
    opstinaRodjenjaID: number;
    id: number;
    ime: string;
    email: string;
    prezime: string;
    opstinaPrebivalistaID: number;
    slikaStudentaNew?: any;
    slikaStudentaCurrent?: any;
  }

