import {Component, OnInit} from '@angular/core';
import {StudentPrikazVM, StudentRow} from './student-prikaz-vm';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MyConfig} from './MyConfig';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {



  studentPrikaz:StudentPrikazVM=null;
  trazi: string='';
  editStudent: StudentRow;

  currentPage: number=1;
  itemsPerPage:number=10;
  pageNumbersArray():number[]{
    let result=[];
    if (this.studentPrikaz != null) {
      for (let i = 0; i < this.totalPages(); i++)
        result.push(i+1);
    }
    return result;
  }

  private totalPages() {
    return this.studentPrikaz.total / this.itemsPerPage;
  }

  goToPage(p: number) {
    this.currentPage = p;
    this.preuzmiPodatke();
  }

  goPrev() {
    if (this.currentPage>1)
      this.currentPage--;

    this.preuzmiPodatke();
  }

  goNext() {
    if (this.currentPage<this.totalPages())
      this.currentPage++;

    this.preuzmiPodatke();
  }

  constructor(private http:HttpClient) {

  }

  preuzmiPodatke() {

    this.http.get<StudentPrikazVM>(MyConfig.adresaServer+ "/Student2/Index?currentPage="+this.currentPage+"&itemsPerPage="+this.itemsPerPage).subscribe((result)=>{
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
    return this.studentPrikaz.studenti.filter(s=>s.ime.startsWith(this.trazi));
  }

  generisiPreview() {
    // @ts-ignore
    let file = document.getElementById("fileSlika").files[0];

    if (file)
    {
      var reader = new FileReader();

      //let student = this.editStudent;
      reader.onload = function ()
      {
        let s = reader.result.toString();
        document.getElementById("previewImg").setAttribute("src", s);
        // student.slikaStudentaNew = s;
      }

      reader.readAsDataURL(file);
    }
  }


}

