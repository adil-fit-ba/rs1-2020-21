import {Component, OnInit} from '@angular/core';
import {StudentPrikazVM, StudentRow} from './upravljanje-studenti/uredi-student/student-prikaz-vm';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MyConfig} from './myconfig';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  ngOnInit(): void {
  }



}

