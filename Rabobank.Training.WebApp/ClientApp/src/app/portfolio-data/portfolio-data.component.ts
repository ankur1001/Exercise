import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-portfolio-data',
  templateUrl: './portfolio-data.component.html'
})
export class PortfolioDataComponent {
  public apiValues: PortfolioVM[];
  public positions: PositionVM[];
  public mandates: MandateVM[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<PositionVM[]>(baseUrl + 'portfolio').subscribe(result => {
      this.positions = result;
    }, error => console.error(error));
  }
}

interface PortfolioVM {
  positions: PositionVM[];
}

interface PositionVM {
  code: number;
  name: string;
  value: number;
  mandates: MandateVM[];
}

interface MandateVM {
  name: string;
  allocation: number;
  value: number;
}
