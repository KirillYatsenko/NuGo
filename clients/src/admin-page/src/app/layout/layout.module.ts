import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [LayoutComponent, NavbarComponent],
  imports: [
    CommonModule,
    RouterModule,
  ],
  exports: [LayoutComponent, NavbarComponent]
})
export class LayoutModule { }
