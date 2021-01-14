import { Component, OnInit } from '@angular/core';
import {StudentPrikazVM, StudentRow} from "./uredi-student/student-prikaz-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../myconfig";

@Component({
  selector: 'app-upravljanje-studenti',
  templateUrl: './upravljanje-studenti.component.html',
  styleUrls: ['./upravljanje-studenti.component.css']
})
export class UpravljanjeStudentiComponent implements OnInit {


  studentPrikaz:StudentPrikazVM=null;
  trazi: string='';
  editStudent: StudentRow;
  currentPage: number=1;
  pageSize: number=10;

  constructor(private http:HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmiPodatke()
  }

  preuzmiPodatke() {

    this.http.get<StudentPrikazVM>(MyConfig.adresaServer+ "/Student2/Index?currentPage=" + this.currentPage+"&q="+this.trazi).subscribe((result)=>{
      this.studentPrikaz = result;

    });
  }

  obrisi(s: StudentRow) {
    this.http.get(MyConfig.adresaServer+ "/Student2/Obrisi?StudentID="+s.id).subscribe((result)=>{
      //  alert('obrisano ' + s.ime);
      let indexOf = this.studentPrikaz.studenti.indexOf(s);
      this.studentPrikaz.studenti.splice(indexOf, 1);
    });
  }

  uredi(s: StudentRow) {
    this.editStudent = s
  }

  getStudenti():StudentRow[] {
    return this.studentPrikaz.studenti//.filter(s=>s.ime.startsWith(this.trazi));
  }


  pageNumberChanged($event: any) {
    this.preuzmiPodatke();
  }

  traziButton() {
    this.currentPage=1;
    this.preuzmiPodatke();
  }
}
