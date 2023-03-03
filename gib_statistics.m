data = dlmread('times_optimized_20_50000.txt', ',');



% Vytvoření dvojic [x,y]
rada = zeros(1, length(data));
for i = 1:length(data)
    rada(i) = 20 + i;
end




plot(rada,data);

xlim([0 length(data)]);
ylim([0 max(data)]);

xlabel('Vstupní čísla')
ylabel('Trvání faktorizace [ms]')
title('Faktorizace')
grid on


m = median(data);
gr = (data(end) - data(1)) / length(data);
kurt = kurtosis(data);
s = std(data);
mea = mean(data)

fprintf("Median = %d\n", m);
fprintf("Míra růstu = %d\n", gr);
fprintf("Koeficient špičatosti = %d\n", kurt);
fprintf("Směrodatná odchylka = %d\n", s);
fprintf("Střední hodnota = %d\n", mea);