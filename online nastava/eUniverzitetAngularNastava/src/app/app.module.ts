import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UrediStudentComponent } from './upravljanje-studenti/uredi-student/uredi-student.component';
import { UpravljanjeStudentiComponent } from './upravljanje-studenti/upravljanje-studenti.component';
import { PostavkeComponent } from './postavke/postavke.component';
import { RouterModule } from '@angular/router';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    UrediStudentComponent,
    UpravljanjeStudentiComponent,
    PostavkeComponent,
  ],
    imports: [
        BrowserModule,
        FormsModule,
      NgbModule,
      HttpClientModule,
      RouterModule.forRoot([
        {path: 'otvori-postavke', component: PostavkeComponent},
        {path: 'otvori-studenti', component: UpravljanjeStudentiComponent},
      ])
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
