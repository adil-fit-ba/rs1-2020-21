
export class StudentRow {
  id: number;
  brojIndeksa: string;
  ime: string;
  prezime: string;
  opstinaPrebivalista: string;
  opstinaRodjenja: string;
  email: string;
}

export class StudentPrikazVM {
  studenti: StudentRow[];
  q?: any;
}
