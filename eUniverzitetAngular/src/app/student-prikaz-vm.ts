
export interface StudentRow {
  id: number;
  brojIndeksa: string;
  ime: string;
  prezime: string;
  opstinaPrebivalista: string;
  opstinaRodjenja: string;
  email: string;
}

export interface StudentPrikazVM {
  studenti: StudentRow[];
  q?: any;
}
