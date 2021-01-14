import {Component, OnInit} from '@angular/core';
import {StudentPrikazVM, StudentRow} from './student-prikaz-vm';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MyConfig} from './MyConfig';
import {PagingInfo} from './pagingInfo';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent extends PagingInfo{

  studentPrikaz:StudentPrikazVM=null;
  trazi: string='';
  editStudent: StudentRow;


  constructor(private http:HttpClient) {
    super();
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



  getTotalItems(): number {
    if (this.studentPrikaz == null)
      return 0;

    return this.studentPrikaz.total;
  }


}



