import { Component } from '@angular/core';
import { Repository } from "./models/repository";
import { Movie } from "./models/movie.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Web Development with ASP.NET Core 3.0 & Anglar 7';
  constructor(private repo: Repository) { }
  get movie(): Movie {
    return this.repo.movie;
  }
}
