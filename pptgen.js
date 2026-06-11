const fs = require('fs');
const PptxGenJS = require('pptxgenjs');
const md = fs.readFileSync('SLIDE.md','utf8');
const lines = md.split(/\r?\n/);
const pptx = new PptxGenJS();
const slide = pptx.addSlide();
slide.background = { fill: 'FFFFFF' };
// Title
let i = 0;
let title = '';
for (; i < lines.length; i++) {
  if (lines[i].trim().length === 0) continue;
  title = lines[i].replace(/^#\s*/,'').trim(); i++; break;
}
slide.addText(title, { x:0.5, y:0.3, w:9, h:1, fontSize:28, bold:true, align:'center' });
// Content
let y = 1.5;
for (; i < lines.length; i++) {
  const line = lines[i].trim();
  if (!line) { y += 0.25; continue; }
  if (line.startsWith('- ')) {
    slide.addText('• ' + line.substring(2), { x:1.0, y: y, fontSize:14, w:8, h:0.5 });
    y += 0.45;
  } else if (line.endsWith(':')) {
    slide.addText(line, { x:0.75, y: y, fontSize:14, bold:true, w:8 });
    y += 0.35;
  } else {
    slide.addText(line, { x:0.75, y: y, fontSize:12, w:8 });
    y += 0.35;
  }
}

pptx.writeFile({ fileName: 'PRESENTATION.pptx' })
  .then(() => console.log('PRESENTATION.pptx written'))
  .catch((err) => { console.error(err); process.exit(1); });
