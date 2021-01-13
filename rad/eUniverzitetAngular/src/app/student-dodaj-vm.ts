
  export interface Opstine {
     text: string;
     value: string;
  }

  export interface StudentDodajVM {
    opstine: Opstine[];
    opstinaRodjenjaID: string;
    id: number;
    ime: string;
    email: string;
    prezime: string;
    opstinaPrebivalistaID: string;
    slikaStudentaNew?: any;
    slikaStudentaCurrent?: any;
  }

