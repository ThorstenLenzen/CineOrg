import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  navLinks: any[];
  activeLinkIndex = 0;

  constructor(private router: Router) {
    this.navLinks = [
      {
        label: 'Home',
        link: './home',
        icon: 'home',
        index: 0
      }, {
        label: 'Movies',
        link: './movies',
        icon: 'movie',
        index: 1
      }, {
        label: 'Theaters',
        link: './theaters',
        icon: 'theaters',
        index: 2
      }, {
        label: 'Log Entries',
        link: './logs',
        icon: 'data_usage',
        index: 3
      },
    ];
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
  }

}
