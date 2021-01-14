import {Component, Input, OnInit} from '@angular/core';
import {PagingInfo} from './pagingInfo';

@Component({
  selector: 'app-mypaging',
  templateUrl: './mypaging.component.html',
  styleUrls: ['./mypaging.component.css']
})
export class MypagingComponent implements OnInit {

  @Input() x:PagingInfo;

  constructor() { }

  ngOnInit(): void {
  }

}
