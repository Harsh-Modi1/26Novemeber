import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title: string = 'prjonlineshopping';
  loginsession: boolean;
  userName: string = '';

  constructor(private router: Router) { }

  ngOnInit(): void {
    
  }

  ngDoCheck() {
    if (sessionStorage.getItem('email')) {
      this.loginsession = true;
      this.userName = sessionStorage.getItem('userName');
    } else {
      this.loginsession = false;
    }
  }

  logOff() {
    sessionStorage.clear();
    this.loginsession = false;
    this.router.navigate(['/home']);
  }
}
