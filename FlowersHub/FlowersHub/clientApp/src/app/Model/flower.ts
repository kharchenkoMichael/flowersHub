export class Flower {
  url?: string;
  title?: string;
  description?: string;
  currency?: string;
  priceDouble?: number;
  imageUrl?: string;
  group?: string;

  constructor(data: any) {
    this.url = (data && data.url) ? data.url : null;
    this.title = (data && data.title) ? data.title : null;
    this.description = (data && data.description) ? data.description : null;
    this.currency = (data && data.currency) ? data.currency : null;
    this.priceDouble = (data && data.priceDouble) ? data.riceDouble : null;
    this.imageUrl = (data && data.imageUrl) ? data.imageUrl : null;
    this.group = (data && data.group) ? data.group : null;
  }
}
